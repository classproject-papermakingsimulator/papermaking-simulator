package cn.edu.sjtu.shareservice.Service;

import cn.edu.sjtu.shareservice.Entity.SharedPicture;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.multipart.MultipartFile;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public interface SharedPictureService {

	String save(MultipartFile file);

	byte[] get(int num);

	public List<Map> image();

	public byte[] getImage (String id);
    
}