package cn.edu.sjtu.test2;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
@SpringBootApplication
public class DemoApplication {

    public static void main(String[] args) {
        SpringApplication.run(DemoApplication.class, args);
    }

    @Autowired
    private RestTemplate restTemplate;

    @Bean
    public RestTemplate getRestTemplate() {
        return new RestTemplate();
    }

    @RequestMapping("/serviceBInfo")
    public String serviceBInfo() {
        System.out.println("serviceBInfo  start");
        return "serviceB  被调用了";
    }

    @RequestMapping("/getServiceA")
    public String getServiceA() {
        System.out.println("开始访问servicea");
        return restTemplate.getForObject("http://localhost:8202/serviceAInfo", String.class);
    }

}