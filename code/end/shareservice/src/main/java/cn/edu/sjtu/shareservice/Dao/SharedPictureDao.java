package cn.edu.sjtu.shareservice.Dao;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import cn.edu.sjtu.shareservice.Entity.SharedPicture;

public interface SharedPictureDao {

	SharedPicture save(SharedPicture target);

	List<SharedPicture> findAll();


}