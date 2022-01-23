﻿Нижний слой - DAL - Data Assess Layer - отвечает только за сохранение/загрузку каких-то данных (Entities). Ведь код находится в Server.DAL, нужно использовать IRepository. ВСЕ слои выше используют этот интерфейс - смысл многослойки в том, чтобы слои не зависели от реализации друг друга.

Средний слой - BLL - Business Logic Layer - отвечает за логику приложения. То есть тут сосредоточен весь "код", все "алгоритмы", все возможные "действия" с данными, которые нужны в рамках данной задачи. Данный слой работает с Entities, не с Dto.

Можно заметить, что всё работает асинхронно, то есть использует ключевые слова async, await и Task. Это усложняет жизнь, но по идее сервер не будет медленным.

В проекте есть дтошки. Они нужны для верхнего слоя. Там будут контроллеры и всё, связанное с сетевым взаимодействием, можно сказать, там будет Asp.NET. В этом слое мапятся Dto в Entities и обратно. По сути, этот выглядит, как обертка над функциями из BLL.

Насчет BLL. Мы решили, что пусть Id будут внутри Entities и генерируются в момент создания сущности.