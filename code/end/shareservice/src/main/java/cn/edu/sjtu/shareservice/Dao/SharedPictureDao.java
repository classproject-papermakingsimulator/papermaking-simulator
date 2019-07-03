package cn.edu.sjtu.shareservice.Dao;

import java.util.List;

import cn.edu.sjtu.shareservice.Entity.SharedPicture;

public interface SharedPictureDao {

	SharedPicture save(SharedPicture target);

	List<SharedPicture> findAll();
    
}