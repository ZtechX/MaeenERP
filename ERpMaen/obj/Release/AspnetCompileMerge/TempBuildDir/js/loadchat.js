//////////////// Global Variables //////////////////////////
        var online_users = "";
        var message_history = "";
        var current_user_id = 0;
        var all_current_users = [];
        var istyping_users = [];
        var flag_istyping = [];
        var istyping_status = false;

        $(function () {
                            //setInterval(function () { update_users_status() }, 5000);
                       //     setInterval(function () { chakchat() }, 2000);
//                            setInterval(function () { checkistyping() }, 10000);
                           // Update_currentuser_satus();
        });
        //////////////// window load get user to list //////////////////////////
        window.onload = function () {
            //get_online_usersonload();
        };
        ////////////////  get user to list for first time  //////////////////////////
/*
         function get_online_usersonload() {
             WebService.get_online_users('no', Set_Users);
             function Set_Users(val) {

                 for (var i = 0; i <= val.length - 1; i++) {
                     username = val[i].split("|")[0];
                     userstatus = val[i].split("|")[3];
                     var userstatusid = "userstatus" + username;
                     x = document.getElementById(userstatusid);
                     if (x != null) {

                         if (userstatus) {
                             x.className = "profile-status online";
                         }
                         else {
                             x.className = "profile-status offline";
                         }
                     }
                 }
                 set_user_to_array(val);
             }
         }
 */
        //////////////// update users status online and offline users   //////////////////////////
        function update_users_status() {
            WebService.get_online_users('no', Set_Users);
            function Set_Users(val) {
                for (var i = 0; i <= val.length - 1; i++) {
                    username = val[i].user_name;
                    userstatus = val[i].On_Off;
                    var userstatusid = "userstatus" + username;
                    x = document.getElementById(userstatusid);
                    if (x != null) {

                        if (userstatus == "True") {
                            x.className = "profile-status online";
                        }
                        else {
                            x.className = "profile-status offline";
                        }
                    }
                }
            }
        }
        ////////////////  save istyping status    ////////////////////////////////////////
        function saveistyping(value, reciever_id) {
            WebService.saveistyping(reciever_id);
        }
        /////////////////////empty is typing table/////////////////////////////////////////
        function empty_istyping() {
            WebService.empty_istyping();
        }
        ///////////////////////get is typing status for specific chatter /////////////////////////
        function istyping(reciever_id) {
            WebService.Getistyping(reciever_id, istypintstatus);
            function istypintstatus(val) {
                istyping_status = val
            }
        }
        //////////////// empty istyping table when no typing   //////////////////////////
        function checkistyping() {
            empty_istyping();
            flag_istyping = [];
        }
        //////////////// add user to istyping list  //////////////////////////
        function useristyping(user_id) {
            var index = flag_istyping.indexOf(user_id);
            if (index == -1) {
                flag_istyping.push(user_id);
                saveistyping(0, user_id);
            }
        }
        //////////////// add users to users list   //////////////////////////
        function set_user_to_array(val) {
            online_users = val;
            $(function () {
                $.chat({
                    // your user information
                    userId: 1,
                    // id of the room. The friends list is based on the room Id
                    roomId: 1,
                    // text displayed when the other user is typing
                    typingText: ' is typing',
                    // title for the rooms window
                    roomsTitleText: 'Rooms',
                    // title for the 'available rooms' rab
                    availableRoomsText: 'Available rooms',
                    // text displayed when there's no other users in the room
                    emptyRoomText: "There's no one around here. You can still open a session in another browser and chat with yourself :)",
                    // the adapter you are using
                    chatJsContentPath: '/basics/chatjs/',
                    adapter: new DemoAdapter()
                });
            });
        }
        //////////////// save messages between chatters   //////////////////////////
        function savechat(user_id, message) {
            WebService.savechat(user_id + "|" + message);
            current_user_id = user_id;
        }
        //////////////// display messages between chatters //////////////////////////
        function displaychat(val) {
            var user = "#" + current_user_id;
            $(user).html('');
            var username = "";
            $("#ChatMessageList").html('');
            message_history = '';
            for (var i = 0; i <= val.length - 1; i++) {
                username = val[i].split("|")[3];
                var user_id = val[i].split("|")[0];
                var message = val[i].split("|")[1];
                var time = val[i].split("|")[2];
                debugger;
                if (i == 0) {
                    if (user_id == username) {

                        message_history = "<div class='sender'><Label>" + user_id + "</label>" + message +"</br>";
                    }
                    else {
                        message_history = "<div class='recipient'><Label>" + user_id + "</label>" + message +"</br>";
                    }
                    if (i == val.length - 1) {
                        message_history = message_history + "</div>";
                        $(user).append(message_history);
                    }
                }
                else {
                    if (i == val.length - 1) {

                        if (val[i].split("|")[0] != val[i - 1].split("|")[0]) {
                            if (user_id == username) {

                                message_history =message_history+ "</div> <div class='sender'><Label>" + user_id + "</label>" + message + " </div>";
                            }
                            else {
                                message_history =message_history + "</div><div class='recipient'><Label>" + user_id + "</label>" + message + " </div>";
                            }
                        }

                        else {

                                message_history  = message_history + message + " </div>";
                             }

                        $(user).append(message_history);
                    }
                    else {
                        if (val[i].split("|")[0] != val[i - 1].split("|")[0]) {
                            message_history = message_history + "</div>";
                            $(user).append(message_history);
                            if (user_id == username) {
                                message_history = "<div class='sender'><Label>" + user_id + "</label>" + message + "</br>" ;
                            }
                            else {
                                message_history = " <div class='recipient'><Label>" + user_id + "</label>" + message+ "</br>";
                            }
                        }
                        else {
                            message_history = message_history + message+"</br>";
                        }
                    }
                }
                
            }      
            istyping(current_user_id);
            if (istyping_status == "True") {
                var is = "<p class='typing-signal'>" + username + ' typing ...</p>';
                $(user).append(is);
            }
        }
        //////////////// Get Chat And new messages between chatters  //////////////////////////
/*
         function chakchat() {
             WebService.getchat(current_user_id + "|" + 0, getchat);
             WebService.chek_new_messages(current_user_id + "|" + 0, check_new_messages);
             function getchat(val) {
                 displaychat(val);
             }
             function check_new_messages(val) {
                 for (var i = 0; i <= val.length - 1; i++) {
                     var sender_id = val[i].split("|")[0];
                     var index = all_current_users.indexOf(sender_id);
                     if (index == -1) {
                         $("#userlist" + sender_id).click();
                         all_current_users.push(sender_id);
                     }
                 }
             }
         }
 */
        //////////////// get witch user current user  caht with him  //////////////////////////
        function Get_Current_User_Chat(user_id) {
            current_user_id = user_id;
            all_current_users.push(user_id);
            //chakchat();
        }
        //////////////// when close chatter window remove it from current user  //////////////////////////
        function remove_user_from_current_users() {
            for (var i = 0; i <= all_current_users.length - 1; i++) {
                var user = all_current_users[i];
                var myElem = document.getElementById(user);
                if (myElem == null) {
                    var index = all_current_users.indexOf(user);
                    if (index > -1) {
                        all_current_users.splice(index, 1);
                        istyping_users.splice(index, 1);
                    }
                }
            }
        }
        function Update_currentuser_satus() {
                WebService.update_user_status(1);
        }
        function myFunction() {
            return WebService.update_user_status(0);
        }