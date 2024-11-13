extends Control
onready var FishingPlus = $"/root/FishingPlus"

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	_set_buttons()
	_add_fish()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _add_fish():
	for item in Globals.item_data.keys():
		if Globals.item_data[item]["file"].category != "fish":
			continue
		if Globals.item_data[item]["file"].loot_table == "deep":
			continue
		
		
		var lob = $Panel/Panel2/ScrollContainer/VBoxContainer
		var bad = Globals.item_data[item]["file"].item_name
		var item_id = item
		
		var lb = preload("res://mods/FishingPlus/ui/plus/bad_button.tscn").instance()
		var selected = false			
		if item_id in FishingPlus.current_fish: selected = true
		lb._setup(bad, selected)
		lob.add_child(lb)
		lb.connect("_pressed", self, "_add_fish_id", [item_id])

func _set_buttons():
	$Panel/percent.text = ("On" if FishingPlus.percent == true else "Off")
	$Panel/possible.text = ("On" if FishingPlus.possible == true else "Off")
	$Panel/patient.text = ("On" if FishingPlus.patient == true else "Off")
	$Panel/auto.text = ("On" if FishingPlus.autobait == true else "Off")
	$Panel/autocollect.text = ("On" if FishingPlus.autocollect == true else "Off")
						
	
func _open():
	if $Panel.visible == true:
		$Panel.visible = false
	else:
		$Panel.visible = true
func _close():
	$Panel.visible = false
# below here is the most slop yandere sim code that i have ever wrote
# i honestly just wanted to write it as fast as possible
func _add_fish_id(fish_id):
	if fish_id in FishingPlus.current_fish:
		FishingPlus.current_fish.erase(fish_id)
	else:
		FishingPlus.current_fish.append(fish_id)
	FishingPlus._update_config()

func _on_percent_pressed():
	if FishingPlus.percent == true:
		$Panel/percent.text = "Off"
		FishingPlus.percent = false
		FishingPlus._update_config()
	else:
		$Panel/percent.text = "On"
		FishingPlus.percent = true
		FishingPlus._update_config()


func _on_possible_pressed():
	if FishingPlus.possible == true:
		$Panel/possible.text = "Off"
		FishingPlus.possible = false
		FishingPlus._update_config()
	else:
		$Panel/possible.text = "On"
		FishingPlus.possible = true
		FishingPlus._update_config()


func _on_patient_pressed():
	if FishingPlus.patient == true:
		$Panel/patient.text = "Off"
		FishingPlus.patient = false
		FishingPlus._update_config()
	else:
		$Panel/patient.text = "On"
		FishingPlus.patient = true
		FishingPlus._update_config()


func _on_autobait_pressed():
	if FishingPlus.autobait == true:
		$Panel/auto.text = "Off"
		FishingPlus.autobait = false
		FishingPlus._update_config()
	else:
		$Panel/auto.text = "On"
		FishingPlus.autobait = true
		FishingPlus._update_config()


func _on_autocollect_pressed():
	if FishingPlus.autocollect == true:
		$Panel/autocollect.text = "Off"
		FishingPlus.autocollect = false
		FishingPlus._update_config()
	else:
		$Panel/autocollect.text = "On"
		FishingPlus.autocollect = true
		FishingPlus._update_config()



func _on_select_push():
	if FishingPlus.fishnotify == true:
		$Panel/fishselect.text = "Off"
		$Panel/Panel2.visible = false
		$Panel/LineEdit.visible = false
		$Panel/SearchLabel.visible = false
		FishingPlus.fishnotify = false
		FishingPlus._update_config()
	else:
		$Panel/fishselect.text = "On"
		$Panel/Panel2.visible = true
		$Panel/LineEdit.visible = true
		$Panel/SearchLabel.visible = true
		FishingPlus.fishnotify = true
		FishingPlus._update_config()


func _on_LineEdit_text_changed(new_text):
	var lob = $Panel/Panel2/ScrollContainer/VBoxContainer

	for child in lob.get_children(): child.queue_free()
	
	for item in Globals.item_data.keys():
		var loot_table = Globals.item_data[item]["file"].loot_table
		if Globals.item_data[item]["file"].category != "fish":
			continue
		if loot_table == "deep" or loot_table == "metal":
			continue
		
		
		var bad = Globals.item_data[item]["file"].item_name
		var item_id = item
		if bad.to_lower().find(new_text.to_lower()) != -1:
			var lb = preload("res://mods/FishingPlus/ui/plus/bad_button.tscn").instance()
			var selected = false			
			if item_id in FishingPlus.current_fish: selected = true
			lb._setup(bad, selected)
			lob.add_child(lb)
			
			lb.connect("_pressed", self, "_add_fish_id", [item_id])


func _on_LineEdit_focus_entered():
	FishingPlus.on_focus_type = true
func _on_LineEdit_focus_exited():
	FishingPlus.on_focus_type = false
