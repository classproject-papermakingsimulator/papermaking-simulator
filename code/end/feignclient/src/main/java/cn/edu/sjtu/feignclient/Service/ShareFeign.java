package cn.edu.sjtu.feignclient.Service;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.feignclient.Entity.User;

@FeignClient(value = "service-share")
public interface ShareFeign {
    
    @RequestMapping("/")
    public String home();

    @RequestMapping(value = "/share", method = RequestMethod.POST, produces="application/json;charset=UTF-8")
    @ResponseBody
    String share(@RequestParam(value = "file") MultipartFile file, @RequestParam(value = "user") User user);

    @RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    @ResponseBody
    byte[] show(@RequestParam(value = "num") int num);
}