#include <pthread.h>
#include <stdio.h>
#include <signal.h>
#include <unistd.h>

static int count = 0;
static pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int increment_counter()
{
    pthread_mutex_lock(&mutex);
    printf("Mutex locked\n");
    sleep(1);
    count++;
    int result = count;
    pthread_mutex_unlock(&mutex);
    return result;
}

void *thread_function(void *arg)
{
    for (int i = 0; i < 10; ++i)
    {
        int value = increment_counter();
        printf("Thread %d incremented counter to %d\n", *(int *)arg, value);
    }
    return NULL;
}

void signal_handler(int signal)
{
    int value = increment_counter();
    printf("Signal received. Counter incremented to %d\n", value);
}

int main()
{
    signal(SIGINT, signal_handler);

    pthread_t threads[3];
    int thread_ids[3] = {1, 2, 3};

    for (int i = 0; i < 3; ++i)
    {
        pthread_create(&threads[i], NULL, thread_function, &thread_ids[i]);
    }

    for (int i = 0; i < 3; ++i)
    {
        pthread_join(threads[i], NULL);
    }

    return 0;
}
