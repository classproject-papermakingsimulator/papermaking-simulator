package cn.edu.sjtu.shareservice.DaoImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import cn.edu.sjtu.shareservice.Dao.SharedPictureDao;
import cn.edu.sjtu.shareservice.Entity.SharedPicture;
import cn.edu.sjtu.shareservice.Respository.SharedPictureRespository;

@Repository
public class SharedPictureDaoImpl implements SharedPictureDao {

    @Autowired
    private SharedPictureRespository sharedPictureRespository;

    @Override
    public SharedPicture save(SharedPicture target) {
        return sharedPictureRespository.save(target);
    }
}