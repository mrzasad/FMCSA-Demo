# FMCSA-Demo
Global Search for Federal Motor Carrier Safety Administration demo

Code Configuration
In order to configure FMCSA database repository, please Select Presentation Project as Startup project. Pass correct connection-string inside appsettings.Development.json.
Select Tools >> Package Manage Console >> Package Manager Console 
Use the following command to run pre-configured migration: update-database 
Above command will create Database along with schema.

Next step is to seed data in the database. You can find  seed-fmcsa-demo-data.sql in Core >> Seed-SQL-Data >> seed-fmcsa-demo-data.sql Please execute seed-fmcsa-demo-data.sql script to seed data.

Use the following credentials to login:
admin@admin.com
Admin@123

**Login Screen**
![image](https://user-images.githubusercontent.com/1267014/118507991-c222b380-b6fc-11eb-9012-d2c74c94e2a5.png)

By default, application is populating data based on default vehicle type i.e Large Bus and State: IL

![image](https://user-images.githubusercontent.com/1267014/118508267-09a93f80-b6fd-11eb-8fa8-5e391308f253.png)

**Multi-Select controls** have been used to enhance filter criteria that is:
 > User can search for multiple vehicle types
 > Same is the case for States as well. 
 ![image](https://user-images.githubusercontent.com/1267014/118508474-3a897480-b6fd-11eb-9ea1-2f8ef5c016ca.png)
 ![image](https://user-images.githubusercontent.com/1267014/118508532-483efa00-b6fd-11eb-9590-313ab9b03d83.png)

Beside advanced filtering feature, it also supports to extract data as a **CSV, Excel, PDF and direct printing**
![image](https://user-images.githubusercontent.com/1267014/118509116-c7ccc900-b6fd-11eb-8e6e-8a910ce5e4ea.png)

