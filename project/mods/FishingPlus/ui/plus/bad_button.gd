extends Control

signal _pressed

func _setup(fish_name, selected):
	$Panel / HBoxContainer / Label.bbcode_text = str(fish_name)
	if selected == true: 
		$Panel / HBoxContainer / Button.text = "delete"

func _pressed():
	emit_signal("_pressed")
