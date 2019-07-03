package cn.edu.sjtu.shareservice.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.shareservice.Entity.User;
import cn.edu.sjtu.shareservice.Service.SharedPictureService;

@RestController
public class ShareController {

    @Autowired
    private SharedPictureService sharedPictureService;

    @Value("${server.port}")
    String port;

    @RequestMapping("/")
    public String home() {
        return "hello world from port " + port;
    }

    @RequestMapping(value = "/share", method = RequestMethod.POST, produces="application/json;charset=UTF-8")
    @ResponseBody
    public String share(@RequestBody MultipartFile file, User user) {
        return sharedPictureService.save(file, user);
    }
}