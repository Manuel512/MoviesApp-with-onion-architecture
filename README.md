# MoviesApp-with-onion-architecture
Microsoft Movies application with onion architecture with these features:

- BackEnd
  Features Added
    - Builded with a N-tier architecture uncoupling concerns in projects
      one project for handling the web requests, another for the core/bussiness of the App 
      including services, interfaces and entities and for last the infrastructure layer
      that keep the context and repository access for the app.
    - Added Integration tests for good scenarios against a database in memory
    - Added builtin DI from entity core for the interfaces in the business layer
    - Added Autofac to inject dependencies in test Project

  Pending features
    - Unit tests with mock for bad scenarios
    - Add autofac provider to startup of application

- FrontEnd
  Features Added
    - Angular 8 app builtin in Visual studio 2019 templates
    - Added bootstrap 4.5 for css layouts
    - Added jquery for bootstrap and some modals functionality
    - Added Services and injected in components
    - Add basic crud functionality with four components MovieList, AddMovie, EditMovie and MovieDetails
    - Added delete functionality in MovieDetails
    - All crud actions are notified by a modal message with confirmation

  Pending Features
    - Form validation in addMovie and EditMovie
    - Best use of templates in components
    - Remove hardcoded elements for Movie languages
    - Include more UI features/libraries for best UX
