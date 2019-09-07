package cn.edu.sjtu.feignclient.Service;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.feignclient.Config.FeignSupportConfig;
import cn.edu.sjtu.feignclient.Config.ShareServiceHystrix;

import java.util.List;
import java.util.Map;


@FeignClient(value = "service-share", configuration = FeignSupportConfig.class, fallback = ShareServiceHystrix.class)
public interface ShareFeign {
    
    @RequestMapping("/")
    public String home();

    @PostMapping(value = "upload", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    String share(@RequestPart(value = "file") MultipartFile file);

    //@RequestMapping(value = "/show", method = RequestMethod.GET, produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    //byte[] show(@RequestParam(value = "num") int num);

    @GetMapping("/file/img")
    @ResponseBody
     List<Map> image();

    @GetMapping(value = "/show/{id}", produces = {MediaType.IMAGE_JPEG_VALUE, MediaType.IMAGE_PNG_VALUE})
    @ResponseBody
    byte[] getImage(@PathVariable String id);
}