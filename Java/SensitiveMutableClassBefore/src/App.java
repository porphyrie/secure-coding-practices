public class App {
    public static void main(String[] args) throws Exception {
        SubmittedUserForm userData = new SubmittedUserForm();
        UserService.updateUser(userData);
        UserService.displayUser(userData.userId);
    }
}
