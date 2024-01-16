public class App {
    public static void main(String[] args) throws UnsupportedOperationException {
        try {
            SubmittedUserForm userData = new SubmittedUserForm();
            UserService.updateUser(userData);
            UserService.displayUser(userData.userId);
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }
}
