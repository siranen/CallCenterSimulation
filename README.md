# CallCenterSymulation - In Progress

#Purpuse
A model of a call center with one type of agents with different skills and many types of incoming calls. The model focuses of call routing, if the caller is waiting for too long. The calls arrive at certain rates and are placed in queues (one queue for each call type). Some callers abandon from the queue after a certain waiting time, some don't have enough information.
+ Make a report from working under subject
+ Test skills of workers with targets
+ Help determine if more lines are needed

#Implemented
+ Basic Functions are working
+ Reports are being shown in numbers
+ All information input and output is by LINQ's

#Not implemented (To be added)
There are two agent groups, each trained to handle a particular call type. However the agents are also cross-trained so that they can handle calls of different type, yet less efficiently.  The logic for call routing is the following: a call is routed to the “native” agent, if there is one available; otherwise, the call is routed to the “alien” agent, again if the latter is idle. The output metrics in this model are the queue lengths and “service levels” for both call types. All parameters can be changed on-the-fly, including the routing options.

#Settings Menu - Schedule / Agents / Timings
![alt text](https://s13.postimg.io/bd7s3zq2f/2016_08_25_14_33_48_Startowa.png)

#Settings Menu - Subjects for Agents - Timings
![alt text](https://s13.postimg.io/5qbf6injr/2016_08_25_14_31_27_Startowa.png)

#Run with settings paramteres
![alt text](https://s13.postimg.io/ju2p165dj/2016_08_25_14_38_18_Centrum_akcji.png)


