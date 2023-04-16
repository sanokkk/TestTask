Тестовое задание
API, работающее с информацией о жителях города Х.
- 2 метода контроллера:
  1) Получает всех жителей города (информация выводится без возраста) по указанным фильтрам: возраст (левая и правая границы),
  пол (женский/мужской, без указания фильтр не применяется). Выводит информацию по страницам.
  2) Получает жителя по ID, информация выводится с возрастом.
- Заполнение базы данных (PostgreSQL) при запуске приложения (если данные имеются, ничего не изменяется) данными по ссылкам из условия задания.
- Unit-тесты (библиотека XUnit): тестируются методы контроллера (проверяется код ответа на запрос), методы репозитория (проверяется корректность работы репозитория с данными).
