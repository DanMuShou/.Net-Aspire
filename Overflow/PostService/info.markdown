.
├── Contracts
│   ├── PostCreated.cs
│   ├── PostDeleted.cs
│   ├── PostUpdated.cs
│   ├── BlogCreated.cs
│   └── BlogUpdated.cs
├── Overflow.AppHost
│   └── ... (保持不变)
├── Overflow.ServiceDefaults
│   └── ... (保持不变)
├── PostService  // 重命名自QuestionService
│   ├── Controllers
│   │   ├── PostsController.cs  // 通用帖子控制器
│   │   ├── QuestionsController.cs  // 问题类帖子
│   │   └── BlogsController.cs  // 博客专用控制器
│   ├── Data
│   │   ├── Migrations
│   │   └── PostDbContext.cs  // 扩展为支持博客数据
│   ├── Dtos
│   │   ├── Post
│   │   │   ├── CreatePostDto.cs
│   │   │   ├── UpdatePostDto.cs
│   │   │   └── PostResponseDto.cs
│   │   └── Blog
│   │       ├── CreateBlogDto.cs
│   │       ├── UpdateBlogDto.cs
│   │       └── BlogResponseDto.cs
│   ├── Models
│   │   ├── Post
│   │   │   ├── PostBase.cs  // 基础帖子模型
│   │   │   ├── PostQuestion.cs  // 问题类帖子
│   │   │   └── PostBlog.cs  // 博客类帖子
│   │   └── BaseModel.cs
│   ├── Repositories
│   │   ├── Post
│   │   │   ├── IPostRepository.cs
│   │   │   ├── IPostQuestionRepository.cs
│   │   │   └── IPostBlogRepository.cs
│   │   └── IBaseRepository.cs
│   ├── Services
│   │   ├── Post
│   │   │   ├── PostService.cs
│   │   │   ├── PostQuestionService.cs
│   │   │   └── PostBlogService.cs
│   │   └── TagService.cs
│   ├── Validators
│   │   ├── Post
│   │   │   ├── PostValidator.cs
│   │   │   ├── PostQuestionValidator.cs
│   │   │   └── PostBlogValidator.cs
│   │   └── ...
│   └── ... (其他配置文件)
└── SearchService
    ├── MessageHandlers
    │   ├── PostCreatedHandler.cs
    │   ├── BlogCreatedHandler.cs
    │   └── ... (其他事件处理器)
    └── ...