public class ImmutableUser extends User {
    ImmutableUser(User user) {
        super(user);
    }

    @Override
    public char[] getBankAccountNumber() {
        return super.getBankAccountNumber().clone();
    }

    @Override
    public void setBankAccountNumber(char[] bankAccountNumber) {
        throw new UnsupportedOperationException();
    }
}