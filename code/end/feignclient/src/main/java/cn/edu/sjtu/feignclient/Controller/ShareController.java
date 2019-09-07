package cn.edu.sjtu.feignclient.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.feignclient.Service.ShareFeign;

import java.util.List;
import java.util.Map;

@RestController
public class ShareController {

    @Autowired
    private ShareFeign shareFeign;

    @RequestMapping("/")
    public String home() {
        return shareFeign.home();
    }

    @RequestMapping(value = "/share", method = RequestMethod.POST, consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public String share(@RequestPart(value = "file") MultipartFile file) {
		return shareFeign.share(file);
    }

    //@RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    //public byte[] show(@RequestParam(value = "num") int num){
     //   return shareFeign.show(num);
    //}

    @RequestMapping(value = "/file/img", method = RequestMethod.GET)
    public List<Map> image(){
        return shareFeign.image();
    }

    @RequestMapping(value = "/show/{id}", method = RequestMethod.GET)
    @ResponseBody
    public byte[] getImage(@PathVariable String id){
        return shareFeign.getImage(id);
    }
}