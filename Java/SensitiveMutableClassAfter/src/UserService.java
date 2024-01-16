public class UserService {
    public static void updateUser(SubmittedUserForm form) {
        // pretend to retrieve data from the db based on userId
        User user = new User();
        // updated other fields
        user.setBankAccountNumber(form.getBankAccountNumber());
        // pretend to update the db
        System.out.println(user.getBankAccountNumber());
    }

    public static void displayUser(int userId) {
        // pretend to retrieve data from the db based on userId
        ImmutableUser user = new ImmutableUser(new User());
        renderUser(user);
    }

    public static void renderUser(ImmutableUser user) {
        // programmer is no longer allowed to change data
        user.setBankAccountNumber("BCR1234567887654321".toCharArray());
        System.out.println(user.getBankAccountNumber());
    }
}