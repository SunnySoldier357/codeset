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
    "categories": [
        "C#"
    ]
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