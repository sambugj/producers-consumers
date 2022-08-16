# producers-consumers
Implementations in a  C# Console app for the purpose of learning how to solve the "Producers and Consumers" concurrent problem step by step using lock to ensure the mutual exclusion to access in the critical region.

We have implementations with issues to see the differences with the thread safe implementation.

Running the bad implementations we can see different errors with the same program and that is typically a behaviour of the program with concurrency problems.

DataBuffer Project
------------------
Here we have different implementations to the buffer. 
You could add another implementation (using mutex) and inject that to test if it is thread safe or not. 

MainProgram Project
-------------------
The MainProgram project implements the runner process of the producers and consumers threads and has a monitor process to log information on the console to see the progress and the results.
  
  -) Program.cs: it is to configure the test to execute. The console app starts executing this source file. 
     Using the 'enum TestCase' you will select the implementation to run changing the following line:
       var testCase = TestCase.List_Unsafe;

ThreadProducerConsumer Project
------------------------------
Here we have two implementation, unsafe and safe using lock. You could use mutex to create a new implementation.
