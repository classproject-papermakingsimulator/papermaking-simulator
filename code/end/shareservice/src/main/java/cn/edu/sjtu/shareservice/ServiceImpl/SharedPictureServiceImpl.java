package cn.edu.sjtu.shareservice.ServiceImpl;

import java.io.IOException;
import java.util.Date;

import org.bson.types.Binary;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.shareservice.Dao.SharedPictureDao;
import cn.edu.sjtu.shareservice.Entity.SharedPicture;
import cn.edu.sjtu.shareservice.Entity.User;
import cn.edu.sjtu.shareservice.Service.SharedPictureService;

public class SharedPictureServiceImpl implements SharedPictureService {

    @Autowired
    private SharedPictureDao sharedPictureDao;

    @Override
    public String save(MultipartFile file, User user) {
        try {
            SharedPicture target = new SharedPicture();
            target.setName(user.getUsername());
            target.setCreatedTime(new Date());
            target.setContent(new Binary(file.getBytes()));
            target.setContentType(file.getContentType());
            target.setSize(file.getSize());
            sharedPictureDao.save(target);
            return "1";
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "0";
    }
    
}