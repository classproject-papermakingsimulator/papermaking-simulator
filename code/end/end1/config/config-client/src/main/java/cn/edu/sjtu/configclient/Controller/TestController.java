package cn.edu.sjtu.configclient.Controller;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.cloud.context.config.annotation.RefreshScope;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RefreshScope
public class TestController {

    @Value("${name}")
    private String nickName;

    @RequestMapping("/hello")
    public String hello() {
        return "hello " + nickName;
    }

}