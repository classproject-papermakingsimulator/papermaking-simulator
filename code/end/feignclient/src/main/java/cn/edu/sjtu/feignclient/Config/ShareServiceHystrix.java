package cn.edu.sjtu.feignclient.Config;

import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

import cn.edu.sjtu.feignclient.Service.ShareFeign;

@Component
public class ShareServiceHystrix implements ShareFeign {

    @Override
    public String home() {
        return "service is wrong";
    }

    @Override
    public String share(MultipartFile file) {
        return "service is wrong";
    }

    @Override
    public byte[] show(int num) {
        return null;
    }
}
