- Нижний слой - DAL - Data Assess Layer - отвечает только за сохранение/загрузку каких-то данных. Ведь код находится в Server.DAL, там расположен интерфейс репозитория и единственная его реализация в виде EfRepository.

- Средний слой - BLL - Business Logic Layer - отвечает за логику приложения. То есть тут сосредоточен весь "код", все "алгоритмы", все возможные "действия" с данными, которые нужны в рамках данной задачи. Есть несколько сервисов, они работают с нужными репозиториями, а также могут обращаться к другим сервисам.

- Верхний слой - ASP.net-обертка над сервисами из BLL, тут прописаны возможные эндпоинты.

DAL сделан асинхронным, и асинхронщина тянется по слоям наверх.

![image02](https://user-images.githubusercontent.com/75442282/150738584-4f337b7e-fc52-45ec-ad52-6cc211e08892.png)
