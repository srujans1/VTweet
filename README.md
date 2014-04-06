VTweet by Srujan Saggam and Anshul Mehra
======

VTweet is a social networking site powered by AWS(EC2,S3,Elastic Beanstalk,Elastic Transcoder ,Cloudfront,RDS). It creates an ultimate social networking experience by letting users start video conversations and follow other users in their network. This is a mobile friendly site with html5 media queries.

Live Website link : http://vtweet.elasticbeanstalk.com
 
 The following workflows have been acheived: <br/>
==> user can register or login using local database or OAuth Authentication.<br/>
==> User can stary Video coversations.<br/>
==> User can follow/unfollow other users<br/>
==> User can see feed of the users that he is following.<br/>
==> User can view his uploaded videos.<br/>
==> The cloudfront distribution for the video url is stored in the database not the original one.<br/>
==> When the video is uploaded, apart from original one a copy of transcoded video is stored on the cloud. The respective videos are served based on the devices that are used to access the site. <br/>
