$(function () {
    var i = true;
    // change the Azure Mobile service client connection details 
    var client = new WindowsAzure.MobileServiceClient('https://futureunleashed.azure-mobile.net/', 'kaafHWyhvDEsCMppcwXPKYGsBg62'),
                feedbackTable = client.getTable('Feedback2');
    var eventFeedbackTable = client.getTable('eventfeedback');

    $('#add-item').bootstrapValidator();  //read up

    // Handle insert;
    $('#add-item').submit(function (evt) {
        try {

            if (i == true) {

                var itemText1 = getRadioValue('SessionRating');         //function getRadioValue is written below
                var itemText2 = getRadioValue('SpeakerRating');
                var itemText5 = getRadioValue('OverallRating');

                 //function getRadioValue is written below
                var itemText7 = getRadioValue('Complete');
                var itemText8 = getRadioValue('Recommend');

                var uidtextbox = $('#uuid'),
                uid = uidtextbox.val();

                if (uid == "" || itemText1 == undefined || itemText2 == undefined || itemText5 == undefined ||  itemText7 == undefined || itemText8 == undefined) {
                    return;
                }

                //document.getElementById("imgsplash").src = "http://i.imgur.com/KUJoe.gif";
                var qs = location.search.substring(1);
                var Qs_Value = qs.split("&");
                var s = Qs_Value[0]
                var q = Qs_Value[1];

                var sarray = s.split("=");
                var sid = sarray[1];
                var sessionarray = q.split("=");

                var session = sessionarray[1];

                if (Qs_Value.length > 2)
                    session = Qs_Value[1] + " And " + Qs_Value[2];
                else
                    session = sessionarray[1];

                itemText = sid;

                //var uidtextbox = $('#uuid'),
                //uid = uidtextbox.val();         //getting the user registration ID
                var DetailsText = $('#Details'),
                    itemText3 = DetailsText.val();      //getting additional details/comments

                var platform = 4;       //4 means web here

                //if (uid == undefined)
                //    return;

                if (itemText !== '') {
                    i = false;          //means everything is ok


                    document.getElementById("imgsplash").src = "http://i.imgur.com/KUJoe.gif";



                    feedbackTable.insert({ sessionid: itemText, sessionrating: itemText1, speakerrating: itemText2, textfeedback: itemText3, platformid: platform, overallrating: itemText5, uuid: uid }).then(refreshNothing, handleError);
                    eventFeedbackTable.insert({ uuid: uid,  q2_complete: itemText7, q3_recommend: itemText8 }).then(refreshTodoItems, handleError);
                }
            }

            evt.preventDefault();
        }

        catch (err) {
            document.getElementById("result").innerHTML = "Error submitting feedback, try again!"
        }
    });

    //Dom parse for Radio Value
    function getRadioValue(theRadioGroup) {
        var elements = document.getElementsByName(theRadioGroup);
        for (var i = 0, l = elements.length; i < l; i++) {
            if (elements[i].checked) {
                return elements[i].value;
            }
        }
    }

    function refreshNothing() {
        //document.getElementById("result").innerHTML = "";
    }

    function refreshTodoItems() {
        //document.getElementById("result").innerHTML = "Thanks! Your feedback has been successfully recorded.<br> <span style='font-weight:bold'> If you are interested in Azure, and would like to contact us, please <a href='http://azureconferencecontactus.azurewebsites.net/'> click here </a> </span>";
        document.getElementById("result").innerHTML = "Thanks! Your feedback has been successfully recorded.";

        document.getElementById("feedbackbtn").disabled = true;
        $(".preload").fadeOut(2000, function () {
            $(".content").fadeIn(1000);
        });
    }

    function handleError(error) {
        document.getElementById("result").innerHTML = "Error submitting feedback due to network issues. Try again by closing and reopening the page.";
        $(".preload").fadeOut(2000, function () {
            $(".content").fadeIn(1000);
        });
    }
});