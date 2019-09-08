package cn.edu.sjtu.shareservice.Entity;

import org.bson.types.Binary;

public class User {

    private String userId;
    private String username;
    private Binary protait;
    private String password;

    /**
     * @return the password
     */
    public String getPassword() {
        return password;
    }
    /**
     * @param password the password to set
     */
    public void setPassword(String password) {
        this.password = password;
    }
    /**
     * @return the protait
     */
    public Binary getProtait() {
        return protait;
    }
    /**
     * @param protait the protait to set
     */
    public void setProtait(Binary protait) {
        this.protait = protait;
    }
    /**
     * @return the userId
     */
    public String getUserId() {
        return userId;
    }
    /**
     * @param userId the userId to set
     */
    public void setUserId(String userId) {
        this.userId = userId;
    }
    /**
     * @return the username
     */
    public String getUsername() {
        return username;
    }
    /**
     * @param username the username to set
     */
    public void setUsername(String username) {
        this.username = username;
    }
}