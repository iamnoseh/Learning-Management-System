
# 📑 ТЗ барои LMS (Learning Management System)

## 1. Ҳадафи система
Сохтани LMS, ки ба истифодабарандагон имконият медиҳад:
- курсҳои онлайн гиранд,
- видео, файл, тестҳо омӯзанд,
- пешрафти худро бубинанд,
- бо дигарон дар форум муҳокима кунанд,
- ва бо моделҳои “Free” ва “Premium” кор кунанд.

## 2. Ролҳои истифодабарандагон
### Админ
- Идоракунии истифодабарандагон, курсҳо, пардохтҳо.
- Dashboard бо статистика.

### Муаллим (Instructor)
- Эҷод ва идоракунии курсҳо.
- Иловаи видео, файл, саволномаҳо (quiz).
- Муошират бо донишҷӯён (форум/шарҳ).

### Донишҷӯ (Student)
- Сабти ном, ворид шудан.
- Дастрасӣ ба курсҳои ройгон/премиум.
- Гузаронидани дарсҳо, тестҳо.
- Дидани пешрафт ва сертификатҳо.

## 3. Функсионал
### 3.1. Authentication ва Authorization
- ASP.NET Identity + JWT
- Social login (Google, Facebook optional)
- Role-based access (Admin, Instructor, Student)

### 3.2. Системаҳои курсҳо
- CRUD курсҳо (ном, тавсиф, нарх, категория)
- Видео-дарсҳо (upload / embed YouTube/Vimeo)
- Файлҳо (PDF, DOCX, PPTX)
- Тестҳо ва Quiz
- Assignment (супоришҳо, project submission)

### 3.3. Subscription ва Billing
Модел:
- 15 рӯз ройгон → баъдан пардохт (premium)
- Пардохт тавассути OsonPay/Payme/Stripe

Нақшаҳо (Plans):
- Monthly, Yearly
- Trial (15 days)

### 3.4. Tracking (Progress System)
Донишҷӯ мебинад:
- % курс гузаштааст
- кадом дарсҳоро тамом кардааст
- балҳои тестҳо
- сертификат (PDF download)

### 3.5. Discussion Board (Forum/Comments)
- Форум барои курсҳо (Q&A, муҳокима)
- Комментарияҳо зери ҳар дарс
- Upvote/Downvote барои ҷавобҳо

### 3.6. Notifications
- Email (welcome, пардохт, progress)
- Telegram бот (optional)
- In-app notifications

### 3.7. Dashboard
- **Admin Dashboard:** истифодабарандаҳо, даромад, курсҳо
- **Instructor Dashboard:** донишҷӯёни курс, рейтинг, assignment submissions
- **Student Dashboard:** пешрафт, курсҳои гирифташуда, огоҳиномаҳо

## 4. Архитектура
- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor Server ё Blazor WASM (Client → API)
- **Database:** PostgreSQL
- **Auth:** Identity + JWT
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, Presentation)
- **ORM:** EF Core + Dapper (барои performance queries)
- **File Storage:** LocalStorage / Cloud (S3, Azure Blob)
- **Deployment:** Docker + Ubuntu (Nginx reverse proxy)

## 5. Non-functional requirements
- **Performance:** API < 200ms response time
- **Security:** JWT, HTTPS, Password hashing, Role-based access
- **Scalability:** имконият барои Load Balancing
- **Logging & Monitoring:** Serilog + ELK Stack / Seq

## 6. Roadmap (Этапҳо)
- **MVP:** Authentication, CRUD курсҳо, subscription, тамошои видео.
- **Phase 2:** Тестҳо, tracking, dashboard.
- **Phase 3:** Форум, огоҳиномаҳо.
- **Phase 4:** Scaling, optimizations, mobile app (optional).

## 📊 ERD барои LMS
### 📌 Ҷадвалҳо ва майдонҳо

#### 1. Users (истифодабарандагон)
- Id (PK, GUID)
- FirstName
- LastName
- Email (UNIQUE)
- PasswordHash
- Role (Admin/Instructor/Student)
- CreatedAt

#### 2. Courses (курсҳо)
- Id (PK)
- Title
- Description
- Category
- Price
- IsFree (bool)
- CreatedBy (FK → Users.Id)
- CreatedAt

#### 3. Lessons (дарсҳо)
- Id (PK)
- CourseId (FK → Courses.Id)
- Title
- Content (матн)
- VideoUrl
- FileUrl
- OrderIndex (тартиб)

#### 4. Enrollments (сабти донишҷӯ)
- Id (PK)
- CourseId (FK → Courses.Id)
- StudentId (FK → Users.Id)
- EnrollmentDate
- IsPremium (bool)
- ExpiryDate

#### 5. Progress (пешрафт)
- Id (PK)
- EnrollmentId (FK → Enrollments.Id)
- LessonId (FK → Lessons.Id)
- IsCompleted (bool)
- CompletedAt

#### 6. Quizzes (тестҳо)
- Id (PK)
- CourseId (FK → Courses.Id)
- Title
- Description

#### 7. Questions (саволҳо)
- Id (PK)
- QuizId (FK → Quizzes.Id)
- QuestionText
- QuestionType (SingleChoice/MultiChoice/Text)

#### 8. Answers (ҷавобҳо ба саволҳо)
- Id (PK)
- QuestionId (FK → Questions.Id)
- AnswerText
- IsCorrect (bool)

#### 9. Submissions (натиҷаи донишҷӯён)
- Id (PK)
- QuizId (FK → Quizzes.Id)
- StudentId (FK → Users.Id)
- SubmittedAt
- Score

#### 10. DiscussionPosts (форум/шарҳҳо)
- Id (PK)
- CourseId (FK → Courses.Id)
- UserId (FK → Users.Id)
- Content
- ParentId (nullable, FK → DiscussionPosts.Id)
- CreatedAt

#### 11. Subscriptions (пардохтҳо ва планҳо)
- Id (PK)
- UserId (FK → Users.Id)
- PlanType (Trial/Monthly/Yearly)
- StartDate
- EndDate
- IsActive (bool)
- PaymentReference

### 📌 Муносибатҳо (Relationships)
- User ↔ Courses → 1-to-Many (Instructor → Courses)
- User ↔ Enrollments → Many-to-Many (Student ↔ Course)
- Course ↔ Lessons → 1-to-Many
- Course ↔ Quizzes → 1-to-Many
- Quiz ↔ Questions ↔ Answers → Hierarchical
- Student ↔ Submissions → Many-to-Many (Student ↔ Quiz)
- Course ↔ DiscussionPosts → 1-to-Many (threaded with ParentId)
- User ↔ Subscriptions → 1-to-Many
