# HttpSandbox
Web/ Mock http server with GUI. 

## Features:
- lightweight C# scripts (runtime compilable) to generate JSON data from databases (**PostgeSQL**) using **Dapper**
- quick web sites prototyping
  
<img width="1471" height="727" alt="image" src="https://github.com/user-attachments/assets/2f405105-3e3f-4e1f-ae31-d7ed6bee2f76" />

<img width="1098" height="558" alt="image" src="https://github.com/user-attachments/assets/bb94334a-b8cb-40e9-8320-88cc9ffa461c" />




## Samples:
Tabulator + Dapper + PostgeSQL [tabulator sample](Samples/tabulator_sample.sxml)


## How to run static page 
1. Presets -> Hello world
3. Run, apply
4. Open browser and enter "http://localhost:8888"
### To change static page content ###
1. Select Mock tab 
2. Call context menu on the first row (StaticHtmlPageResponse)
3. Commands -> load HTML from file
4. Select html file
5. Press F5 in a browser in order to update page
