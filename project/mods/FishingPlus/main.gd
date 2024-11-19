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



var gdweave_directory := _get_directory()
var configs_directory := gdweave_directory.plus_file("configs")
var _dir := Directory.new()
var _file := File.new()

func _ready():
	_get_settings()
	pass # Replace with function body.
	
		
func _get_directory() -> String:
	var game_directory := OS.get_executable_path().get_base_dir()
	var default_directory := game_directory.plus_file("GDWeave")
	var folder_override: String
	var final_directory: String
	
	for argument in OS.get_cmdline_args():
		if argument.begins_with("--gdweave-folder-override="):
			folder_override = argument.trim_prefix("--gdweave-folder-override=").replace("\\", "/")
	
	if folder_override:
		var relative_path := game_directory.plus_file(folder_override)
		var is_relative := not ":" in relative_path and _file.file_exists(relative_path)
		
		final_directory = relative_path if is_relative else folder_override
	else:
		# if they are using env variables for some reason
		if OS.has_environment("GDWEAVE_FOLDER_OVERRIDE"):
			folder_override = OS.get_environment("GDWEAVE_FOLDER_OVERRIDE").replace("\\", "/")
			var relative_path := game_directory.plus_file(folder_override)
			var is_relative := not ":" in relative_path and _file.file_exists(relative_path)
		
			final_directory = relative_path if is_relative else folder_override
		else:
			final_directory = default_directory
	
	return final_directory

func _get_settings():
	# quick and dirty tacklebox solution
	
	if _dir.open(configs_directory) != OK:
		push_error("Could not open config directory")
		return

	var config_path := configs_directory.plus_file("FishingPlus.json")
	_file.open(config_path, File.READ)
	var config_data := JSON.parse(_file.get_as_text())
	if config_data.error == OK and config_data.result is Dictionary:
		percent = config_data.result["CurrentFishChance"]
		possible = config_data.result["LootTableView"]
		patient = config_data.result["PatientLure"]
		autocollect = config_data.result["AutoCollectBuddies"]
		autobait = config_data.result["AutoSelectBait"]
		fishnotify = config_data.result["NeedCertainFish"]
		current_fish = config_data.result["FishIDs"]
		_file.close()
func _update_config():
	var file = configs_directory.plus_file("FishingPlus.json")
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
