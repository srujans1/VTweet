VTweet by Srujan Saggam and Anshul Mehra
======

VTweet is a social networking site powered by AWS(EC2,S3,Elastic Beanstalk,Elastic Transcoder ,Cloudfront,RDS). It creates an ultimate social networking experience by letting users start video conversations and follow other users in their network. This is a mobile friendly site with html5 media queries.

Live Website link : http://vtweet.elasticbeanstalk.com
 
 The following workflows have been acheived: 
==> user can register or login using local database or OAuth Authentication.
==> User can stary Video coversations.
==> User can follow/unfollow other users
==> User can see feed of the users that he is following.
==> User can view his uploaded videos.
==> The cloudfront distribution for the video url is stored in the database not the original one.
==> When the video is uploaded, apart from original one a copy of transcoded video is stored on the cloud. The respective videos are served based on the devices that are used to access the site. 
