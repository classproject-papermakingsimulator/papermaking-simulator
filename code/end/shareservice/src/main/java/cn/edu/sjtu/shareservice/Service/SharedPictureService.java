package cn.edu.sjtu.shareservice.Service;

import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.shareservice.Entity.User;

public interface SharedPictureService {

	String save(MultipartFile file, User user);
    
}