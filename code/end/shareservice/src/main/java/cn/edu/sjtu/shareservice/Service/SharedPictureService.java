package cn.edu.sjtu.shareservice.Service;

import org.springframework.web.multipart.MultipartFile;

public interface SharedPictureService {

	String save(MultipartFile file);

	byte[] get(int num);
    
}