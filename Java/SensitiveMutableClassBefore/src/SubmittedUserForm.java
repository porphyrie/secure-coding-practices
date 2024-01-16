import java.util.Arrays;

public final class SubmittedUserForm {
    public final int userId = 1;
    // other user fields updated in the form
    private final char[] bankAccountNumber = "ING9876543298765432".toCharArray();

    public char[] getBankAccountNumber() {
        return Arrays.copyOf(bankAccountNumber, bankAccountNumber.length);
    }
}