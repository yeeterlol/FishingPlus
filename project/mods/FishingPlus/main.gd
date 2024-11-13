extends Node

onready var percent = false
onready var possible = false
onready var patient = false
onready var autobait = false
onready var autocollect = false
onready var fishnotify = false
onready var current_fish = []
onready var on_focus_type = false
onready var mod_panel = preload("res://mods/FishingPlus/ui/panel/panel.tscn")
onready var the_button = preload("res://mods/FishingPlus/ui/button/fishingplus.tscn")



var gdweave_directory := OS.get_executable_path() + "/../GDWeave/"
var configs_directory := gdweave_directory + "configs/"
var _dir := Directory.new()
var _file := File.new()

func _ready():
	_get_settings()
	pass # Replace with function body.

func _get_settings():
	# quick and dirty tacklebox solution
	
	if _dir.open(configs_directory) != OK:
		push_error("Could not open config directory")
		return
	
	_dir.list_dir_begin(true, true)
	var file_name := _dir.get_next()
	while file_name != "":
		
		var config_path := configs_directory + file_name
		var mod_id := file_name.replace(".json", "")
		if mod_id == "FishingPlus":
			_file.open(config_path, File.READ)
			var config_data := JSON.parse(_file.get_as_text())
			if config_data.error == OK and config_data.result is Dictionary:
				# slop ass code

				percent = config_data.result["CurrentFishChance"]
				possible = config_data.result["LootTableView"]
				patient = config_data.result["PatientLure"]
				autocollect = config_data.result["AutoCollectBuddies"]
				autobait = config_data.result["AutoSelectBait"]
				fishnotify = config_data.result["NeedCertainFish"]
				current_fish = config_data.result["FishIDs"]
			_file.close()
			break
			
		
		file_name = _dir.get_next()
func _update_config():
	var file = configs_directory + "FishingPlus.json"
	var config_file_err := _file.open(file, File.WRITE)
	if config_file_err != OK:
		return config_file_err
		
	var new_config = {
		"CurrentFishChance": percent,
		"LootTableView": possible,
		"NeedCertainFish": fishnotify,
		"FishIDs": current_fish,
		"AutoSelectBait": autobait,
		"AutoCollectBuddies": autocollect,
		"PatientLure": patient
	}
	
	_file.store_string(JSON.print(new_config, "  "))
	_file.close()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
