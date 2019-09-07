package cn.edu.sjtu.test1;

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

    @RequestMapping("/serviceAInfo")
    public String serviceAInfo() {
        System.out.println("serviceAInfo start");
        return "serviceA  被调用了";
    }

    @RequestMapping("/getServiceB")
    public String getServiceB() {
        System.out.println("开始访问serviceb");
        return restTemplate.getForObject("http://localhost:8203/serviceBInfo", String.class);
    }

}