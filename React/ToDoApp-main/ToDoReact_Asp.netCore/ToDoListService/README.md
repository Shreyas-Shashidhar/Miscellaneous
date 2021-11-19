ToDoApp
=======

Introduction
------------

    • **TodoListReactUI** App is a simple react app, which has features to add a to do item, complete and delete the item. Items are displayed in the order of priority. Built with React, Bootstrap, React-Bootstrap and Axios components for interacting with back end services
    • React app interacts with backend **ToDoListService** which is in ASP.Net Core 3.1.

Features
--------

    • Add an item to the list with priority.
    • Items are displayed in the ascending order of priority (Priority 1 item is displayed first)
    • Mark an item as completed.
    • Delete an item from the list.

Requirements
============

    - Node JS, .Net Core 3.1
    - Text editor ( ex. Visual Studio core, Visual Studio 2019)
    -  qBrowser

Usage
-----

    1. Make sure node.js and git is installed correctly on your operating system
    2. Open your terminal or command prompt then run
        ### clone this repository into your operating system
            git clone https://github.com/shreyas747/ToDoApp.git

    3.  **TodoListReactUI**  Application
            ○ Move to project folder and Open the code in below location using VS Code
             **ToDoReact_Asp.netCore\ToDoListReactUI\todolistreactui**
                
            ○ Install project dependencies
                npm install
                or
                yarn install
            
            ○ Run app in development mode
            npm start
            or
            yarn start
                       it will open http://localhost:3000 in the browser
            
    4. **ToDoListService** Application
            ○ Move to project folder and open the ToDoListApp.sln in Visual studio 2019
               **   \ToDoReact_Asp.netCore\ToDoListService\ToDoListApp**
            ○ Build the application and run app in development mode debug , service will be hosted on 
             IIS express and it will open on http://localhost:37915/api/todoitems  in the browser.
