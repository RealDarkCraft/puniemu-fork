package handler

import (
	"encoding/json"
	"io"
	"log"
	"net/http"

	"github.com/SuperTavor/Puniemu/src/config-manager/configmanager"
	nhnrequests "github.com/SuperTavor/Puniemu/src/nhnRequests"
	"github.com/SuperTavor/Puniemu/src/server/gameserver/createUser/models"
	gameServerModels "github.com/SuperTavor/Puniemu/src/server/gameserver/models"
	userdatamanager "github.com/SuperTavor/Puniemu/src/userDataManager"
)

func Handle(w http.ResponseWriter, r *http.Request) {
	encryptedRequest, err := io.ReadAll(r.Body)
	if err != nil {
		http.Error(w, "No request.", http.StatusBadRequest)
		return
	}
	decryptedRequest, err := nhnrequests.DecodeAndDecrypt(string(encryptedRequest))
	if err != nil {
		http.Error(w, "Bad request encryption/encoding", http.StatusBadRequest)
		return
	}
	var request models.CreateUserRequest
	err = json.Unmarshal([]byte(decryptedRequest), &request)
	if err != nil {
		http.Error(w, "Bad request format", http.StatusBadRequest)
		log.Println(err)
		return
	}
	var generatedUserData = gameServerModels.NewUserData(request.IconID, request.Level5UserID, request.PlayerName)
	marshalledUserData, err := json.Marshal(generatedUserData)
	if err != nil {
		log.Println(err)
		return
	}
	err = userdatamanager.StoreYwpUser(request.DeviceID, request.Level5UserID, "ywp_user_data", marshalledUserData)
	if err != nil {
		log.Println(err)
		return
	}
	err = userdatamanager.StoreYwpUser(request.DeviceID, request.Level5UserID, "ywp_user_tutorial_list", []byte(configmanager.StaticJsons["DefaultTutorialList"]))
	if err != nil {
		log.Println(err)
		return
	}
	response := models.NewCreateUserResponse(configmanager.StaticJsons["DefaultTutorialList"], *generatedUserData)
	marshalledResponse, err := json.Marshal(response)
	if err != nil {
		log.Println(err)
		return
	}
	w.Header().Set("Content-Type", "application/json")
	encryptedMarshalledResponse, _ := nhnrequests.EncryptAndEncode(string(marshalledResponse))
	w.Write([]byte(encryptedMarshalledResponse))
}