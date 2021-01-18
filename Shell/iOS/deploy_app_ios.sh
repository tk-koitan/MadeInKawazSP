#!/bin/sh

echo "Deploy to deploygate"

build_number=`awk -F\" '/"buildNumber"/{print $4}' build_manifest.json`
version_number=`awk -F: '/bundleVersion/{print $2}' ProjectSettings/ProjectSettings.asset | sed -e "s/ //g"`

curl \
 -H "Authorization: token b4a121ffeec56f45443ed62ba915f732b7301e07" \
 -F "file=@$2/build.ipa" \
 -F "message=${version_number} (${build_number})" \
 -F "distribution_key=683d919487f77c62842ee52f614ae72f56e55a33" \
 "https://deploygate.com/api/users/tk_koitan/apps"