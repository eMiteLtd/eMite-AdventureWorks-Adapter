# eMite-AdventureWorks-Adapter

This is the official eMite sample adapter showcasing how to use the **eMite SDK** to develop a custom adapter to onboard data into eMite.

The sample adapter connects to the **Adventure Works** database on an SQL Server, fetches all the Sales Orders and onboards this data into either eMite in the cloud or on premise solution using the eMite SDK. 

eMite adapters are designed as Plugins for the **eMite Adapter Framework** and as such can quickly be developed to access any custom or traditional data to onboarding into the eMite for Analytics and Dashboarding.

eMite adapters can be ran through the eMite Adapter Framework without requiring full eMite installation and as such can be easily used as a **Cloud Agent** to push data from client site (on premise) into the eMite Cloud. 

Below is a general overview of of where the adpaters fall in the overall eMite architecture.

![eMiteArchitectureOverview](Images/eMiteArchitectureOverview.png?raw=true "eMiteArchitectureOverview")
