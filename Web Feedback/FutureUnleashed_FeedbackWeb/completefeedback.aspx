
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="completefeedback.aspx.cs" Inherits="FutureUnleashed_FeedbackWeb.completefeedback" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="IE=11.0000"
          http-equiv="X-UA-Compatible">
    <title>Future Unleashed India 2015 </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="files/jquery.min.js"></script>

    <script src="files/bootstrap.min.js"></script>

    <script src="files/bootstrapValidator.min.js"></script>

    <script src="files/jquery-1.9.1.min.js"></script>

    <script src="files/MobileServices.Web-1.2.2.min.js"></script>
    <!--[if lt IE 9]><script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.6.1/html5shiv.js"></script><![endif]-->
    <link href="files/bootstrap.min.css" rel="stylesheet">

    <style>
        /*
        * Main navigation
        *
        * Turn the `.navbar` at the top of the docs purple.
        */

        .bs-docs-nav {
            margin-bottom: 0;
            background-color: #fff;
            border-bottom: 0;
        }

        .bs-home-nav .bs-nav-b {
            display: none;
        }

        .bs-docs-nav .navbar-brand,
        .bs-docs-nav .navbar-nav > li > a {
            color: #563d7c;
            font-weight: 500;
        }

            .bs-docs-nav .navbar-nav > li > a:hover,
            .bs-docs-nav .navbar-nav > .active > a,
            .bs-docs-nav .navbar-nav > .active > a:hover {
                color: #463265;
                background-color: #f9f9f9;
            }

        .bs-docs-nav .navbar-toggle .icon-bar {
            background-color: #563d7c;
        }

        .bs-docs-nav .navbar-header .navbar-toggle {
            border-color: #fff;
        }

            .bs-docs-nav .navbar-header .navbar-toggle:hover,
            .bs-docs-nav .navbar-header .navbar-toggle:focus {
                background-color: #f9f9f9;
                border-color: #f9f9f9;
            }


        .MainHeaderLogo {
            /*width: 125px;*/
            float: left;
        }

            .MainHeaderLogo .LogoDiv {
                font-size: 28px;
                color: #006dac;
                margin-top: -10px;
            }

            .MainHeaderLogo .LogoDiv2 {
                font-size: 14px;
                color: #006dac;
                float: right;
                padding-top: 0px;
            }
    </style>

    <script>
        function getqueryString() {
            try {

                var qs = location.search.substring(1);
                var Qs_Value = qs.split("&");

                var s = Qs_Value[0];

                if (Qs_Value.length > 2)
                    var q = Qs_Value[1] + " And " + Qs_Value[2];
                else
                    var q = Qs_Value[1];

                var sarray = s.split("=");
                var sid = sarray[1];
                var sessionarray = q.split("=");
                var session = sessionarray[1];

                document.getElementById("SID1").value = sid;
                var uri_dec = decodeURIComponent(session);
                document.getElementById("Session").value = uri_dec;

            } catch (e) {

            }

        }
    </script>

    <meta name="GENERATOR" content="MSHTML 11.00.9600.17631">
</head>
<body onpageshow="getqueryString()">
    <div class="col-sm-offset-1">
        <a class="navbar-brand">
            <div class="MainHeaderLogo" style="margin-top: 10px;">
               <%-- <div class="LogoDiv">Future Unleashed</div>
                <div class="LogoDiv2">India 2015</div>--%>
                <img height="60px" width="99px"  src="files/logofu1.jpg"">
            </div>
        </a>
    </div><br><br>
    <div class="container">
        <div class="col-sm-offset-1" style='font-family: "Segoe UI"; margin-top:60px;'>
            <br>
            <h4>Feedback: </h4>
        </div>
        <form class="form-horizontal" id="add-item" style='font-family: "Segoe UI";'
              method="post" data-bv-feedbackicons-validating="glyphicon glyphicon-refresh"
              data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
              data-bv-feedbackicons-valid="glyphicon glyphicon-ok" data-bv-message="This value is not valid">
            
            <div class="form-group">
                <label class="control-label col-sm-2"
                       for="Session">Session:</label>
                <div class="col-sm-8">
                    <input disabled="" class="form-control" id="Session" type="text" readonly="readonly" placeholder="Session" value=""><input name="sid" id="SID1" type="text" placeholder="Session ID" value="" hidden="hidden">
                </div>
            </div>
            <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-2"
                       for="pwd">Reg Id:</label>
                <div class="col-sm-8"><input name="username" class="form-control" id="uuid" autofocus="" required="" type="number" placeholder="Future Unleashed Registration Id"  value="" data-bv-message="The id is not valid" data-bv-regexp-message="The Registration Id can only be numeric." data-bv-notempty-message="Please enter your correct registration ID"></div>
            </div>
            <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3"
                       for="pwd">Quality of Session Content:</label>
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="radio">
                        <label>
                            <input name="SessionRating" id="SessionRating1"
                                   required="" type="radio" value="4">4 - Excellent
                        </label> <br><label>
                            <input name="SessionRating"
                                   id="SessionRating2" required="" type="radio" value="3"> 3
                        </label>
                        <br><label>
                            <input name="SessionRating" id="SessionRating3" required="" type="radio"
                                   value="2"> 2
              </label><br><label>
              <input name="SessionRating" id="SessionRating4"
                                   required="" type="radio" value="1">1 - Poor
                        </label><br>
                    </div>
                </div>
            </div>
            <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3"
                       for="pwd">Quality of Session Delivery:</label>
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="radio">
                        <label>
                            <input name="SpeakerRating" id="SpeakerRating1"
                                   required="" type="radio" value="4">4 - Excellent
                        </label> <br><label>
                            <input name="SpeakerRating"
                                   id="SpeakerRating2" required="" type="radio" value="3"> 3
                        </label>
                        <br><label>
                            <input name="SpeakerRating" id="SpeakerRating3" required="" type="radio"
                                   value="2"> 2
                        </label><br><label>
                            <input name="SpeakerRating" id="SpeakerRating4"
                                   required="" type="radio" value="1">1 - Poor
                        </label><br>
                    </div>
                </div>
            </div>


            <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3"
                       for="pwd">Overall Session Quality:</label>
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="radio">
                        <label>
                            <input name="OverallRating" id="OverallRating1"
                                   required="" type="radio" value="4">4 - Excellent
                        </label> <br><label>
                            <input name="OverallRating"
                                   id="OverallRating2" required="" type="radio" value="3"> 3
                        </label>
                        <br><label>
                            <input name="OverallRating" id="OverallRating3" required="" type="radio"
                                   value="2"> 2
                        </label><br><label>
                            <input name="OverallRating" id="OverallRating4"
                                   required="" type="radio" value="1">1 - Poor
                        </label><br>
                    </div>
                </div>
            </div>
       

                             <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3" for="pwd">As a result of this event, how likely are you to recommend the use of Microsoft products/solutions to colleagues or peers?</label>
                <div class="col-sm-offset-2 col-sm-10">
                      <div class="radio">
                        <label>
                            <input name="Recommend" id="Recommend1" required="" type="radio" value="4">4 - Very Likely
                        </label> <br>
                          
                          <label>
                            <input name="Recommend" id="Recommend2" required="" type="radio" value="3"> 3
                          </label><br

                        ><label>
                            <input name="Recommend" id="Recommend3" required="" type="radio" value="2"> 2
                        </label><br>
                          
                          <label>
                            <input name="Recommend" id="Recommend4" required="" type="radio" value="1">1 - Unlikely
                        </label><br>

                    </div>
                </div>
            </div>


                 <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3" for="pwd">As a result of this event, how likely are you to complete a web site, business solution, or consumer app (using any OS) that utilizes Azure services in the next 6 months?</label>
                <div class="col-sm-offset-2 col-sm-10">
                      <div class="radio">
                        <label>
                            <input name="Complete" id="Complete1" required="" type="radio" value="4">4 - Very Likely
                        </label> <br>
                          
                          <label>
                            <input name="Complete" id="Complete2" required="" type="radio" value="3"> 3
                          </label><br />

                        <label>
                            <input name="Complete" id="Complete3" required="" type="radio" value="2"> 2
                        </label><br>
                          
                          <label>
                            <input name="Complete" id="Complete4" required="" type="radio" value="1">1 - Unlikely
                        </label><br>

                    </div>
                </div>
            </div>
  

                        <div class="form-group" style='font-family: "Segoe UI";'>
                <label class="control-label col-sm-3"
                       for="pwd">Any other suggestions/feedback?</label> <br><br>
                <div class="col-sm-8 col-sm-offset-2">
                    <div><textarea name="Question_105" class="form-control" id="Details" maxlength="600" rows="4" cols="20"></textarea></div>
                
                    </div>
            </div>




            <div class="form-group" style='font-family: "Segoe UI";'>
                <div class="col-sm-12" style="text-align: center;">
                    <input class="btn btn-primary" id="feedbackbtn" type="submit" value="Submit">
                </div>
            </div><!-- <button type="submit"> Submit</button> -->

            <div class="preload"><img id="imgsplash"></div>
            <div class="col-sm-8">
                <p id="result"style='font-family: "Segoe UI"; font-size: 20px;'></p>
            </div>

            <br><br />

            <div class="container">
                <div class="col-lg-12" style="text-align: center;">
                    <img class="img-responsive"
                         style="margin-right: auto; margin-left: auto;" src="files/microsoftlogo.png">
                </div>
            </div>
            <div style='text-align: center; font-family: "Segoe UI";'>
                Copyright @ Microsoft                 <br><br><a href="http://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/en-us.aspx">Trademark</a>
                <span style="margin: 5px;">|</span>                 
                <a href="http://www.microsoft.com/en-us/legal/intellectualproperty/copyright/default.aspx">
                    Terms of Use
                </a>                 <span style="margin: 5px;">|</span>
                <a href="http://www.microsoft.com/privacystatement/en-in/MicrosoftIndiaWebsites/Default.aspx">
                    Privacy
                    Statement
                </a>                 <span style="margin: 5px;">|</span>
<%--                <a href="http://teched2014@microsoft.com">Contact Us</a>--%>
            </div>
        </form>
    </div>
    <script src="files/bootstrap.min.js"></script>
    <link href="files/bootstrapvalidator.min.css" rel="stylesheet">

    <script src="files/bootstrapvalidator.min(1).js"></script>
    <script src="completefeedback.js"></script>
</body>
</html>