USE [TweetAuthLogin]
GO
drop procedure AddVideo;
drop procedure RemoveVideo;
drop procedure SendFriendRequest;
drop procedure UpdateFriendRequest;
drop procedure AddResponse;
drop function IsFriend;
drop procedure ListUsers;
drop procedure ListFriends;
drop procedure ListVideos;
drop procedure ListResponses;

GO

--Procedure for adding a new video
create procedure AddVideo(
@UserID uniqueidentifier,
@URL text)
as
begin
insert into Video(UserID, URL)
values(@UserID, @URL);
end

GO

--Procedure for removing a video
create procedure RemoveVideo(
@VideoID int
)
as
begin
delete from Video
where VideoID=@VideoID;
end

GO

--Procedure for sending a friend request
create procedure SendFriendRequest(
@UserID uniqueidentifier,
@FriendUserID uniqueidentifier,
@Status varchar(1))
as
begin
insert into Friends(UserID,FriendUserID,Status)
values(@UserID,@FriendUserID,@status);
end

GO

--Procedure for updating friend request or delete a friend
create procedure UpdateFriendRequest(
@UserID uniqueidentifier,
@FriendUserID uniqueidentifier,
@Status varchar(1))
as
begin
 if(@Status='y')
begin
update friends
set status='y' where UserID=@UserID and FriendUserID=@FriendUserID;
    end
 else
begin
delete from friends where UserID=@UserID and FriendUserID=@FriendUserID
end
end

GO

--Procedure for adding a video as response to another video. To be called after response has been added to Video table using AddVideo
create procedure AddResponse(
@OriginalVideoID int,
@ResponseVideoID int)
as
begin
insert into VideoResponse(OriginalVideoID, ResponseVideoID)
values(@OriginalVideoID, @ResponseVideoID);
end

GO

--Prodedure to verify if two users are friends
create function IsFriend(
@UserID uniqueidentifier,
@FriendUserID uniqueidentifier)
returns varchar(1)
as
begin
declare @isfriend uniqueidentifier;
declare @retval varchar(1);
select @isfriend = UserID from Friends where ((UserID=@UserID and FriendUserID=@FriendUserID) or (UserID=@FriendUserID and FriendUserID=@UserID)) and status = 'y';
if(@isfriend is not NULL)
begin
set @retval = 'y';
end 
else
begin
set @retval = 'n';
end
return @retval;
end

GO

--Procedure to list all the users
create procedure ListUsers
as
begin
select * from Users;
end

GO

--Procedure to list all the friends of a particular user
create procedure ListFriends(
@UserID uniqueidentifier)
as
begin
select * from Friends
where UserID=@UserID or FriendUserID=@UserID;
end

GO

--Procedure to list all the videos posted by a particular user
create procedure ListVideos(
@UserID uniqueidentifier)
as
begin
select * from Video
where UserID=@UserID
order by Timestamp;
end

GO

--Procedure to list all the responses to a particular video
create procedure ListResponses(
@VideoID int)
as
begin
select * from VideoResponse
where OriginalVideoID=@VideoID
order by Timestamp;
end