# codeset

**codeset** is a command line tool to simplify maintaining all of
VSCode's settings and extensions over several computers. It uses a
single JSON config file in order to store all of this information.

## codeset's functions

**codeset --install-extensions** : Installs VS Code extensions based on those
specified in the config file ([see below](https://github.com/SunnySoldier357/codeset#sample-config-file-configjson)).
Will respect the categories provided in the config file and will remove any extensions
that are not specified in the config file.
 
## Sample config file (config.json)

The application looks for a JSON configuration file in the
following locations: `$HOME/.config/codeset/config.json`
or `$HOME\.config\codeset\config.json`.

If the categories property is not present, `codeset` will assume that it needs
to monitor all of the categories. The Required category is special in that
regardless if it is specified or not, it will always be monitored and there is
no way to disable it. Put an empty array if you just want to have the Required
categories monitored.

```json
{
    "extensions": {
        "Required": [
            "aaron-bond.better-comments"
        ],

        "C#": [
            "ms-vscode.csharp"
        ]
    },
    "settings": {
        "Required": [
            {
                "key": "better-comments.highlightPlainText",
                "value": false
            }
        ],

        "C#": [
            {
                "key": "razor.disabled",
                "value": true
            }
        ]
    },
    "categories": [
        "C#"
    ]
}
```

Or instead of nesting the files into the config file
itself, separate files can be created for settings &
extensions and the full paths can be linked to the main
`codeset.config.json`.


```json
{
    "extensions": "path to extensions.json",
    "settings": "path to settings.json",
    "categories": [
        "C#"
    ]
}
```

For each setting in the settings portion of the configuration, it must follow
this convention:

```json
{
    "key": "",
    "value": "",
    "instruction": ""
}
```

A few examples are show below...

1. Different values for different Operating Systems

If on Linux or OSX, the value of the setting will be false and vice versa for Windows.

```json
{
    "key": "path-autocomplete.useBackslash",
    "value": {
        "windows": true,
        "linux": false,
        "osx": false
    }
}
```

2. Providing instruction to prompt the user to fill in.

In this case, any value provided will be considered the default value but the
user will still be prompted. This only occurs if the operating system is listed
in the value section.

```json
{
    "key": "java.format.settings.url",
    "value": {
        "windows": "",
        "linux": ""
    },
    "instruction": "File location for Java Formatter (formatter.xml)"
}
```

## Installation

Extract the folder found in the Releases tab and put that in any folder. Add
that folder to `PATH` for the command to work.

## Installation

Extract the folder found in the Releases tab and put that in any folder. Add
that folder to `PATH` for the command to work.

## Dependencies

- [CommandLineParser](https://github.com/commandlineparser/commandline/wiki)
- [Newtonsoft.Json](https://www.newtonsoft.com/json/help/html/Introduction.htm)