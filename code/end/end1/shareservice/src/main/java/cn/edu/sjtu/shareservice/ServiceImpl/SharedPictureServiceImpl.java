package cn.edu.sjtu.shareservice.ServiceImpl;

import java.io.IOException;
import java.util.*;

import org.bson.types.Binary;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.shareservice.Dao.SharedPictureDao;
import cn.edu.sjtu.shareservice.Entity.SharedPicture;
import cn.edu.sjtu.shareservice.Service.SharedPictureService;

@Service
public class SharedPictureServiceImpl implements SharedPictureService {

    @Autowired
    private SharedPictureDao sharedPictureDao;
    @Autowired
    private MongoTemplate mongoTemplate;
    @Override
    public String save(MultipartFile file) {
        String username = file.getOriginalFilename();
        try {
            SharedPicture target = new SharedPicture();
            target.setName(username);
            target.setCreatedTime(new Date());
            target.setContent(new Binary(file.getBytes()));
            target.setContentType(file.getContentType());
            target.setSize(file.getSize());
            sharedPictureDao.save(target);
            System.out.println("ok");
            return "1";
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "0";
    }

    @Override
    public byte[] get(int num) {
        byte[] data = null;
        if(sharedPictureDao.findAll() != null) {
            if (num < sharedPictureDao.findAll().size()) {
                SharedPicture file = sharedPictureDao.findAll().get(num);
                if(file != null){
                    data = file.getContent().getData();
                }
            }
        }
        return data;
    }

    @Override
    public List<Map> image(){
        System.out.println('1');
        HashMap<String, List<String>> updateList = new HashMap<>();
        List<Map> resMap = new ArrayList<Map>();
        List<SharedPicture> file = mongoTemplate.findAll(SharedPicture.class);
        if(file != null){
            for (SharedPicture uploadFile : file) {
                String author_name = uploadFile.getName();

                String img_id=uploadFile.getId();
                if(updateList.containsKey(author_name)){
                    List<String> idList = updateList.get(author_name);
                    idList.add("http://papermakingshare.cn:8900/api/show/"+img_id+"?token=1");
                    updateList.put(author_name,idList);
                }
                else{
                    List<String> idList=new ArrayList<String>();
                    idList.add("http://papermakingshare.cn:8900/api/show/"+img_id+"?token=1");
                    updateList.put(author_name,idList);
                }
            }
            int i=0;
            for (String key : updateList.keySet()) {
                System.out.println("key = " + key);
                Map<String,Object> maptmp=new HashMap<>();
                maptmp.put("names",key);
                maptmp.put("id",i);
                i++;
                maptmp.put("imageid",updateList.get(key));
                System.out.println(maptmp);
                resMap.add(maptmp);
            }
        }
        System.out.println(resMap);
        return resMap;
    }

    @Override
    public byte[] getImage(String id){
        byte[] data = null;
        SharedPicture file = mongoTemplate.findById(id, SharedPicture.class);
        if(file != null){
            data = file.getContent().getData();
        }
        return data;
    }
}