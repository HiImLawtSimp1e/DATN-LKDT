# Website linh kiện điện tử

Đồ án tốt nghiệp nhóm SD-18: Website linh kiện điện tử

Bạn cần cài đặt .NET 6.0 SDK runtime & Node.js 18.0 [*tại đây*](https://nodejs.org/en/download/package-manager) 

## 🚀 Quick start

1.  **Step 1.**
    Clone project bằng [*git bash*](https://git-scm.com/downloads)
    ```sh
    git clone https://github.com/tonnbph26190/DATN_LKDT.git
    ```  
1.  **Step 2.**
    * Chạy backend bằng IIS (port 5000)
    ```sh
    cd ./DATN_LKDT
    ```
    * Change connection string to your database in `shop.Infrastructure/appsettings.json` & `shop.BackendApi/appsettings.json`
    ```sh
     "ConnectionStrings": {
    "DefaultConnection": "server=localhost\\sqlexpress;database=shop_db;trusted_connection=true"
    },
    ```
    * Add migration in `shop.Infrastructure`
    ```
    add-migration InitialDb
    ```
    * Seeding data to database
    ```
    update-database
    ```
    * Run hosting backend with IIS
    ```
    http://localhost:5000
    ```
1.  **Step 3.**
    * Chạy frontend(port 3000)
     ```sh
    cd ./DATN_FE
    ```
    * Install the project dependencies with:
     ```sh
    npm i
    ```
    * Add your cloud name as an environment variable inside `.env.local`:

    ```
    NEXT_PUBLIC_TINYMCE_API_KEY=<Your TinyMCE Key>
    NEXT_PUBLIC_CLOUDINARY_CLOUD_NAME="<Your Cloudinary's Cloud Name>"
    CLOUDINARY_API_KEY="<Your Cloudniary API Key>"
    CLOUDINARY_API_SECRET="<Your Cloundinary API Secret>"
    ```
    * Start the development server frontend with:
    ```
    npm run dev
    ```
