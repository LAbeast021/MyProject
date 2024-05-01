CREATE TABLE [dbo].[User] (
    [UserId] INT IDENTITY(1,1) PRIMARY KEY,
    [Username] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [GoogleId] NVARCHAR(100) NOT NULL,
    [ProfilePicture] NVARCHAR(255),
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE [dbo].[Post] (
    [PostId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [ImageUrl] NVARCHAR(255),
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
    [LikesCount] INT NOT NULL DEFAULT 0,  -- New column for storing the number of likes
    CONSTRAINT [FK_Post_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])
);
CREATE TABLE [dbo].[Like] (
    [LikeId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [PostId] INT NOT NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Like_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]),
    CONSTRAINT [FK_Like_Post_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post]([PostId])
);

