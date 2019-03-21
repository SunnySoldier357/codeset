# codeset

[![Build Status](https://travis-ci.com/SunnySoldier357/codeset.svg?branch=master)](https://travis-ci.com/SunnySoldier357/codeset)

**codeset** is a command line tool to simplify maintaining all of
VSCode's settings and extensions over several computers. It uses a
single JSON config file in order to store all of this information.

## codeset's functions

**codeset -u** : Updates VSCode based on configurations

**codeset config list**: Lists the configuration in a meaningful manner
(default is both --ext or --settings for individual)

**codeset config add**: Adds a configuration

**codeset config remove**: Removes a configuration

**codeset config init**: Creates a template codeset.json file at ~/.config/

**codeset cloud status**: Check status of cloud config and local config

**codeset cloud fetch**

**codeset cloud pull**

Ensure that in the config.json, a list of categories is stored that the user
wants to keep on the local computer. Also have a consistent manner to indicate
different values for different OS and even mention if a particular setting is
only meant for a specific OS
 
## Sample config file (codeset.config.json)

The application looks for a JSON configuration file in the
following locations: `$HOME/.config/codeset.config.json`
or `$HOME\.config\codeset.config.json`.

```json
{
    "extensions": {
        "Required": [
            "aaron-bond.better-comments"
        ]
    },
    "settings": {
        "editor.fontFamily": "Fira Code"
    },
}
```

Or instead of nesting the files into the config file
itself, separate files can be created for settings &
extensions and the full paths can be linked to the main
`codeset.config.json`.


```json
{
    "extensions": "path to extensions.txt",
    "settings": "path to settings.json",
}
```


## Dependencies

- https://github.com/commandlineparser/commandline/wiki
- https://github.com/gsscoder/commandline