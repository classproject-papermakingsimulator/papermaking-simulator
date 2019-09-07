package cn.edu.sjtu.shareservice.Controller;

import cn.edu.sjtu.shareservice.Entity.SharedPicture;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.data.mongodb.core.MongoTemplate;

import cn.edu.sjtu.shareservice.Service.SharedPictureService;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
public class ShareController {

    @Autowired
    private SharedPictureService sharedPictureService;
    @Autowired
    private MongoTemplate mongoTemplate;

    @Value("${server.port}")
    String port;

    @RequestMapping("/")
    public String home() {
        return "hello world from port " + port;
    }

    @PostMapping(value = "upload", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public String share(@RequestPart(value = "file") MultipartFile file) {
        return sharedPictureService.save(file);
    }

    //@RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    //public byte[] show(int num){
        //return sharedPictureService.get(num);
    //}

    @GetMapping("/file/img")
    @ResponseBody
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
                    idList.add("http://localhost:8900/api/show/"+img_id+"?token=1");
                    updateList.put(author_name,idList);
                }
                else{
                    List<String> idList=new ArrayList<String>();
                    idList.add("http://localhost:8900/api/show/"+img_id+"?token=1");
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

    @GetMapping(value = "/show/{id}", produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    @ResponseBody
    public byte[] getImage(@PathVariable String id){
        byte[] data = null;
        SharedPicture file = mongoTemplate.findById(id, SharedPicture.class);
        if(file != null){
            data = file.getContent().getData();
        }
        return data;
    }
}