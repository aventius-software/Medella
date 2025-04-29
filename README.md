# Medella (work in progress)

Medella aims to (one day...) be a complete open source EPR (Electronic Patient Record). It is being designed in mind to be used primarily within the U.K. NHS (National Health Service), whether that be a hospital, GP surgery or community health centre. So, in other words, everywhere for everyone in the NHS ;-)

It is being designed by mostly ex-NHS (and some current NHS) employees, with decades of experience in the NHS in the fields of data engineering, application development, business intelligence, data warehousing and information analysis. With many of us having worked in the NHS for years, we've seen countless expensive, complicated to use (for end users and IT support) systems purchased and implemented only for them not to provide what is really wanted/needed... with this project we aim to try to solve all those issues and more.

Note that this project is currently only in very early stages and currently **not** recommended for any type of production or live usage yet. However, eventually we hope to build something that is reliable, easy to use for both end users and IT professionals (especially for them to manage and maintain).

# Goals

- A free open source alternative to extremely expensive and closed off commercial systems such an EPR (Electronic Patient Record) and/or PAS (Patient Administration System)
- Simple to use for the public, medical administration staff, clinicians, administrators and IT professionals
- Cover all aspects of NHS health support, such as community, primary/secondary care
- Patient facing portal(s) so patients can access their own health record
- Cloud first hosting model which is easy to setup, manage and host, whilst being considerably cheaper than purchasing a new system usually costing in the millions of pounds (GBP)
- Integration with authentication such as Microsoft Entra (Azure AD) for single sign on with existing NHS organisational (eventually this could be using the NHS national tenant)
- Conforming to the latest IG (Information Governance) and data governance standards required by the NHS
- Following data sets, data items and standards laid out in the NHS data dictionary and accompanying datasets
- Back end database and front end applications designed with the latest standards and technology
- Apps that work on desktop, web and mobile devices
- Modular so to be easy to expand to add new modules for services
- Highly testable (e.g. unit testing) applications and databases
- Output for national returns and datasets
- Easy to use and integrate with data warehousing, business intelligence, reporting and analysis. For analysis at local, strategic area and national levels. Maybe eventually something like Azure SQL mirroring to Microsoft Fabric for realtime insights and intelligence.
- Integration with other existing health systems using standards such as FHIR etc...
- Very scalable architecture so you could run a single instance (i.e. in your own hospital), or run it as a 'node' that is part of a resilient group/network. In other words you could run this locally on your own hardware or it could be a national system used by the entire NHS providing all parts of the NHS with a single unified health care system (well, they do say, go big or go home 😃)

We may in addition to this create other projects such as data analysis (as mentioned above) so that realtime insights can be performed (all in accordance with IG) on the data

# Usage

See the accompanying license... 

But basically you are free to use this software, but **completely** at your own risk. None of the creators or collaborators will be held responsible for anything related to the use of this software, whether from bugs in the software or anything else

# Getting started with development

Currently the project is being built using Visual Studio 2022 with .NET 9 and SQL Server database projects. We're trying to design this to be easy to get up and running for development on any local PC (only testing running on Windows 11 so far). Steps to run the system:-

- Install Visual Studio 2022 with latest updates (currently using .NET 9), make sure the web development and database/SQL development parts are selected during install
- Open the database project and run the publish profile, this will deploy the database to the 'debug' database on the (localdb)\ProjectModels server (open Visual Studio's SQL object explorer or click the database project and 'go to debug database')
- Make sure under the 'Apps' folder that the 'Medella' app is set as the 'startup project'
- Click the debug/run button (the button with a green arrow and says 'https') and wait for the app to run 😜

# Contribution

Feel free to get in touch to help
