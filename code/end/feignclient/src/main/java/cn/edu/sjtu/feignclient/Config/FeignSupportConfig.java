package cn.edu.sjtu.feignclient.Config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import feign.codec.Encoder;
import feign.form.spring.SpringFormEncoder;

@Configuration
public class FeignSupportConfig {
  @Bean
  public Encoder feignFormEncoder() {
    return new SpringFormEncoder();
  }
}