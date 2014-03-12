USE [TweetAuthLogin]
alter table [dbo].[Video]
drop constraint fk_video_uid;
alter table [dbo].[VideoResponse]
drop constraint fk_video_ovid;
alter table [dbo].[VideoResponse]
drop constraint fk_video_rvid;

drop table [dbo].[Video];
drop table [dbo].[VideoResponse];
drop table [dbo].[Friends];
GO
CREATE TABLE [dbo].[Video](
	[VideoID] int NOT NULL identity(1,1),
	[UserID] [uniqueidentifier] NOT NULL,
	[URL] text NOT NULL,
	[Timestamp] datetime2 NOT NULL DEFAULT CURRENT_TIMESTAMP,
	constraint fk_video_uid foreign key(UserID) references [dbo].[Users](UserID) ON DELETE CASCADE,
PRIMARY KEY CLUSTERED 
(
    [VideoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[VideoResponse](
	[OriginalVideoID] int NOT NULL,
	[ResponseVideoID]  int NOT NULL,
	[Timestamp] datetime2 NOT NULL DEFAULT CURRENT_TIMESTAMP,
	constraint pk_video_response primary key(OriginalVideoID, ResponseVideoID),
	constraint fk_video_ovid foreign key(OriginalVideoID) references [dbo].[Video](VideoID),
	constraint fk_video_rvid foreign key(ResponseVideoID) references [dbo].[Video](VideoID) ON DELETE CASCADE,
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Friends](
	[UserID] [uniqueidentifier] NOT NULL,
	[FriendUserID] [uniqueidentifier] NOT NULL,
	[Status] varchar(1) NOT NULL,
	[Timestamp] datetime2 NOT NULL DEFAULT CURRENT_TIMESTAMP,
	constraint pk_friends primary key(UserID, FriendUserID),
	constraint fk_users_uid foreign key(UserID) references [dbo].[Users](UserID),
	constraint fk_users_fuid foreign key(FriendUserID) references [dbo].[Users](UserID) ON DELETE CASCADE,
	constraint ck_status check (status in ('y','p'))
) ON [PRIMARY]

select * from Friends;
select * from Video;
select * from VideoResponse;