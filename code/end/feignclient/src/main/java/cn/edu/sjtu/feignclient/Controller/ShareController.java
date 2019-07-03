package cn.edu.sjtu.feignclient.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.feignclient.Entity.User;
import cn.edu.sjtu.feignclient.Service.ShareFeign;

@RestController
public class ShareController {

    @Autowired
    private ShareFeign shareFeign;

    @RequestMapping("/")
    public String home() {
        return shareFeign.home();
    }

    @RequestMapping(value = "/share", method = RequestMethod.POST, produces="application/json;charset=UTF-8")
    @ResponseBody
    public String share(@RequestBody MultipartFile file, User user) {
		return shareFeign.share(file, user);
    }

    @RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    @ResponseBody
    public byte[] show(int num){
        return shareFeign.show(num);
    }
}