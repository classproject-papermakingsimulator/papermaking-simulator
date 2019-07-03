package cn.edu.sjtu.shareservice.Respository;

import org.springframework.data.mongodb.repository.MongoRepository;

import cn.edu.sjtu.shareservice.Entity.SharedPicture;

public interface SharedPictureRespository extends MongoRepository<SharedPicture, Long> {

}