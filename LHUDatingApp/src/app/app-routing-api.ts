import { environment } from "src/environments/environment";
const API = environment.apiUrl;

export class AppRoutingApi {

    // Admin routes
    static LoginAdmin = API + "auths/loginAdmin";

    static GetUsers = API + "admin/getUsers";


    // User routes
    static Login = API + "auths/login";
    static SignUp = API + "auths/register";
    static PostHubConnection = API + "post-hubconnection";
    static DownloadFile = API + "file";

    static GetChatHistory = API + "chatBoards/get-history";
    static GetChatBoardInfo = API + "chatBoards/get-info";
    static AddGroup = API + "chatBoards/groups";
    static SendMessage = API + "chatBoards/send-message";
    static GetMessageByGroup = API + "chatBoards/get-message-by-group";
    static GetMessageByContact = API + "chatBoards/get-message-by-contact";
    static UpdateGroupAvatar = API + "chatBoards/update-group-avatar";

    static GetProfile = API + "users/profile";
    static UpdateProfile = API + "users/profile";
    static UsersLikeMe = API + "users/likeMe";
    static LikeUser = API + "users/like";
    static UnLikeUser = API + "users/unlike";
}
