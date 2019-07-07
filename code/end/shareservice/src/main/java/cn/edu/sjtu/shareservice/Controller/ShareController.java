package cn.edu.sjtu.shareservice.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

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

    @PostMapping(value = "upload", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public String share(@RequestPart(value = "file") MultipartFile file) {
        return sharedPictureService.save(file);
    }

    @RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    public byte[] show(int num){
        return sharedPictureService.get(num);
    }
}